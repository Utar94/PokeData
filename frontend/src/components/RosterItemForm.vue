<script setup lang="ts">
import { TarButton, TarCheckbox, parsingUtils } from "logitar-vue3-ui";
import { computed, inject, onMounted, ref } from "vue";

import type { PokemonType } from "@/types/pokemon";
import type { Region } from "@/types/region";
import type { RosterInfo, SaveRosterItemPayload, SavedRosterItem } from "@/types/roster";
import { saveRosterItem } from "@/api/roster";
import { searchPokemonTypes } from "@/api/pokemon";
import { searchRegions } from "@/api/region";
import { toastsKey, type ToastUtils } from "@/App";

const toasts = inject(toastsKey) as ToastUtils;

const props = defineProps<{
  speciesId: number;
}>();

const loading = ref<boolean>(false);
const payload = ref<SaveRosterItemPayload>({ number: 0, name: "", region: 0, primaryType: 0, isBaby: false, isLegendary: false, isMythical: false });
const regions = ref<Region[]>([]);
const types = ref<PokemonType[]>([]);

const primaryTypes = computed<PokemonType[]>(() => types.value.filter((value) => value.number !== payload.value.secondaryType));
const secondaryTypes = computed<PokemonType[]>(() => types.value.filter((value) => value.number !== payload.value.primaryType));

function fill(fields?: RosterInfo): void {
  payload.value.number = fields?.number ?? 0;
  payload.value.name = fields?.name ?? "";
  payload.value.category = fields?.category ?? "";
  payload.value.isBaby = fields?.isBaby ?? false;
  payload.value.isLegendary = fields?.isLegendary ?? false;
  payload.value.isMythical = fields?.isMythical ?? false;

  const region: Region | undefined = regions.value.find((region) => (region.displayName ?? region.uniqueName) === fields?.region);
  payload.value.region = region?.number ?? 0;

  const primaryType: PokemonType | undefined = types.value.find((type) => (type.displayName ?? type.uniqueName) === fields?.primaryType);
  payload.value.primaryType = primaryType?.number ?? 0;
  const secondaryType: PokemonType | undefined = types.value.find((type) => (type.displayName ?? type.uniqueName) === fields?.secondaryType);
  payload.value.secondaryType = secondaryType?.number;
}
defineExpose({ fill });

function reset(): void {
  payload.value.number = 0;
  payload.value.name = "";
  payload.value.category = undefined;
  payload.value.region = 0;
  payload.value.primaryType = 0;
  payload.value.secondaryType = undefined;
  payload.value.isBaby = false;
  payload.value.isLegendary = false;
  payload.value.isMythical = false;
}

const emit = defineEmits<{
  (e: "saved", saved: SavedRosterItem): void;
}>();
async function submit(): Promise<void> {
  if (!loading.value) {
    loading.value = true;
    try {
      const saved = await saveRosterItem(props.speciesId, payload.value);
      emit("saved", saved);
      reset();
      toasts.success({ text: "The roster item has been saved successfully." });
      loading.value = false;
    } catch (e: unknown) {
      loading.value = false;
      throw e;
    }
  }
}

onMounted(async () => {
  regions.value = (
    await searchRegions({ numberIn: [], search: { terms: [], operator: "And" }, sort: [{ field: "Number", isDescending: false }], skip: 0, limit: 0 })
  ).items;
  types.value = (
    await searchPokemonTypes({
      numberIn: [],
      search: { terms: [], operator: "And" },
      sort: [{ field: "DisplayName", isDescending: false }],
      skip: 0,
      limit: 0,
    })
  ).items;
});
</script>

<template>
  <form @submit.prevent="submit" @reset.prevent="reset">
    <div class="row">
      <div class="col">
        <div class="form-floating mb-3">
          <input class="form-control" id="number" min="1" max="9999" placeholder="Number" required type="number" v-model="payload.number" />
          <label for="number">Number <span class="text-danger">*</span></label>
        </div>
      </div>
      <div class="col">
        <div class="form-floating mb-3">
          <input class="form-control" id="name" maxlength="128" placeholder="Name" required type="text" v-model="payload.name" />
          <label for="name">Name <span class="text-danger">*</span></label>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <div class="form-floating mb-3">
          <input class="form-control" id="category" maxlength="128" placeholder="Category" type="text" v-model="payload.category" />
          <label for="category">Category</label>
        </div>
      </div>
      <div class="col">
        <div class="form-floating mb-3">
          <select
            class="form-select"
            :disabled="regions.length === 0"
            id="region"
            required
            :value="payload.region || ''"
            @input="payload.region = parsingUtils.parseNumber(($event.target as HTMLSelectElement).value) || 0"
          >
            <option value="">Select a region</option>
            <option v-for="region in regions" :key="region.number" :value="region.number">{{ region.displayName ?? region.uniqueName }}</option>
          </select>
          <label for="region">Region <span class="text-danger">*</span></label>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <div class="form-floating mb-3">
          <select
            class="form-select"
            :disabled="types.length === 0"
            id="primary-type"
            required
            :value="payload.primaryType || ''"
            @input="payload.primaryType = parsingUtils.parseNumber(($event.target as HTMLSelectElement).value) || 0"
          >
            <option value="">Select a Pokémon type</option>
            <option v-for="pokemonType in primaryTypes" :key="pokemonType.number" :value="pokemonType.number">
              {{ pokemonType.displayName ?? pokemonType.uniqueName }}
            </option>
          </select>
          <label for="primary-type">Primary Type <span class="text-danger">*</span></label>
        </div>
      </div>
      <div class="col">
        <div class="form-floating mb-3">
          <select
            class="form-select"
            :disabled="types.length === 0"
            id="secondary-type"
            :value="payload.secondaryType || ''"
            @input="payload.secondaryType = parsingUtils.parseNumber(($event.target as HTMLSelectElement).value) || undefined"
          >
            <option value="">Select a Pokémon type</option>
            <option v-for="pokemonType in secondaryTypes" :key="pokemonType.number" :value="pokemonType.number">
              {{ pokemonType.displayName ?? pokemonType.uniqueName }}
            </option>
          </select>
          <label for="secondary-type">Secondary Type</label>
        </div>
      </div>
    </div>
    <TarCheckbox id="is-baby" label="Is Baby?" v-model="payload.isBaby" />
    <TarCheckbox id="is-legendary" label="Is Legendary?" v-model="payload.isLegendary" />
    <TarCheckbox id="is-mythical" label="Is Mythical?" v-model="payload.isMythical" />
    <div class="my-3">
      <TarButton class="me-2" :disabled="loading" :icon="['fas', 'save']" :loading="loading" text="Save" type="submit" />
      <TarButton :disabled="loading" :icon="['fas', 'times']" text="Reset" type="reset" variant="warning" />
    </div>
  </form>
</template>
