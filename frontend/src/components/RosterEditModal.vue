<script setup lang="ts">
import { TarButton, TarCheckbox, TarModal } from "logitar-vue3-ui";
import { computed, ref, watch } from "vue";

import PokemonCategoryInput from "./PokemonCategoryInput.vue";
import PokemonNameInput from "./PokemonNameInput.vue";
import PokemonNumberInput from "./PokemonNumberInput.vue";
import PokemonTypeSelect from "./PokemonTypeSelect.vue";
import RegionSelect from "./RegionSelect.vue";
import RosterItem from "./RosterItem.vue";
import SearchNumberInput from "./SearchNumberInput.vue";
import SearchResultSelect from "./SearchResultSelect.vue";
import SearchTextInput from "./SearchTextInput.vue";
import type { RosterInfo, RosterItem as RosterItemType, SaveRosterItemPayload, SavedRosterItem } from "@/types/roster";
import { saveRosterItem } from "@/api/roster";

const defaults = {
  payload: { number: 0, name: "", category: "", region: "", primaryType: "", secondaryType: "", isBaby: false, isLegendary: false, isMythical: false },
};

const props = defineProps<{
  item?: RosterItemType;
  items: RosterItemType[];
}>();

const loading = ref<boolean>(false);
const modal = ref<InstanceType<typeof TarModal>>();
const payload = ref<SaveRosterItemPayload>({ ...defaults.payload });
const searchNumber = ref<number>(0);
const searchText = ref<string>();
const selectedPokemon = ref<number>();

const hasChanges = computed<boolean>(() => {
  const left = payload.value;
  const right = props.item?.destination ?? { number: 0, name: "", region: "", primaryType: "", isBaby: false, isLegendary: false, isMythical: false };
  return (
    left.number !== right.number ||
    left.name !== right.name ||
    (left.category ?? "") !== (right.category ?? "") ||
    left.region !== right.region.toLowerCase() ||
    left.primaryType !== right.primaryType.toLowerCase() ||
    (left.secondaryType ?? "") !== (right.secondaryType?.toLowerCase() ?? "") ||
    left.isBaby !== right.isBaby ||
    left.isLegendary !== right.isLegendary ||
    left.isMythical !== right.isMythical
  );
});

function onSelected(): void {
  const item: RosterItemType | undefined = props.items.find((item) => item.source.number === selectedPokemon.value);
  setModel(item?.source);
}

function setModel(model?: RosterInfo): void {
  payload.value.number = model?.number ?? 0;
  payload.value.name = model?.name ?? "";
  payload.value.category = model?.category;
  payload.value.region = model?.region.toLowerCase() ?? "";
  payload.value.primaryType = model?.primaryType.toLowerCase() ?? "";
  payload.value.secondaryType = model?.secondaryType?.toLowerCase() ?? "";
  payload.value.isBaby = model?.isBaby ?? false;
  payload.value.isLegendary = model?.isLegendary ?? false;
  payload.value.isMythical = model?.isMythical ?? false;
}

function reset(): void {
  payload.value = { ...defaults.payload };
  searchNumber.value = props.item?.destination?.number ?? props.item?.source.number ?? 0;
  searchText.value = "";
}

const emit = defineEmits<{
  (e: "saved", item: SavedRosterItem): void;
}>();

async function submit(): Promise<void> {
  if (!loading.value && props.item) {
    loading.value = true;
    try {
      const saved = await saveRosterItem(props.item.speciesId, payload.value);
      emit("saved", saved);
      loading.value = false;
      modal.value.hide();
    } catch (e: unknown) {
      loading.value = false;
      throw e;
    }
  }
}

function show(): void {
  modal.value.show();
}
defineExpose({ show });

watch(
  () => props.item,
  (item) => {
    searchNumber.value = item?.destination?.number ?? item?.source.number ?? 0;
    searchText.value = "";
    setModel(item?.destination);
  },
);
watch(searchNumber, () => (selectedPokemon.value = undefined));
watch(searchText, () => (selectedPokemon.value = undefined));
</script>

<template>
  <form @submit.prevent="submit" @reset.prevent="reset">
    <TarModal ref="modal" size="x-large" title="Edit Roster">
      <h3>Source Pokémon</h3>
      <RosterItem v-if="item" :pokemon="item.source" />
      <h3>Destination Pokémon</h3>
      <h4>Search</h4>
      <div class="row">
        <div class="col">
          <SearchNumberInput v-model="searchNumber" />
        </div>
        <div class="col">
          <SearchTextInput v-model="searchText" />
        </div>
      </div>
      <SearchResultSelect :items="items" :search-number="searchNumber" :search-text="searchText" v-model="selectedPokemon" @selected="onSelected" />
      <h4>Properties</h4>
      <div class="row">
        <div class="col">
          <PokemonNumberInput required v-model="payload.number" />
        </div>
        <div class="col">
          <PokemonNameInput required v-model="payload.name" />
        </div>
      </div>
      <div class="row">
        <div class="col">
          <PokemonCategoryInput v-model="payload.category" />
        </div>
        <div class="col">
          <RegionSelect required v-model="payload.region" />
        </div>
      </div>
      <div class="row">
        <div class="col">
          <PokemonTypeSelect :exclude="[payload.secondaryType ?? '']" id="primary-type" label="Primary Type" required v-model="payload.primaryType" />
        </div>
        <div class="col">
          <PokemonTypeSelect :exclude="[payload.primaryType]" id="secondary-type" label="Secondary Type" v-model="payload.secondaryType" />
        </div>
      </div>
      <div class="row">
        <div class="col">
          <TarCheckbox id="is-baby" label="Is Baby?" v-model="payload.isBaby" />
          <TarCheckbox id="is-legendary" label="Is Legendary?" v-model="payload.isLegendary" />
          <TarCheckbox id="is-mythical" label="Is Mythical?" v-model="payload.isMythical" />
        </div>
        <div class="col">
          <div class="my-3 text-end">
            <TarButton :disabled="loading" :icon="['fas', 'times']" text="Reset" type="reset" variant="warning" />
          </div>
        </div>
      </div>
      <template #footer>
        <TarButton :icon="['fas', 'ban']" text="Cancel" variant="secondary" @click="modal.hide()" />
        <TarButton :disabled="loading || !hasChanges" :icon="['fas', 'floppy-disk']" :loading="loading" text="Save" type="submit" />
      </template>
    </TarModal>
  </form>
</template>
