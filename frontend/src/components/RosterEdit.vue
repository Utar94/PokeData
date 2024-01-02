<script setup lang="ts">
import { ref, watch } from "vue";

import RosterItem from "./RosterItem.vue";
import RosterItemForm from "./RosterItemForm.vue";
import SearchNumber from "./SearchNumber.vue";
import SearchResultSelect from "./SearchResultSelect.vue";
import SearchText from "./SearchText.vue";
import type { PokemonRoster, RosterItem as RosterItemType } from "@/types/roster";

const props = defineProps<{
  item: RosterItemType;
  roster: PokemonRoster;
}>();

const form = ref<InstanceType<typeof RosterItemForm>>();
const searchNumber = ref<number>(0);
const searchText = ref<string>();
const selectedPokemon = ref<number>();

watch(searchNumber, () => (selectedPokemon.value = undefined));
watch(searchText, () => (selectedPokemon.value = undefined));

function onSelected(): void {
  if (form.value && selectedPokemon.value) {
    const item: RosterItemType | undefined = props.roster.items.find((item) => item.source.number === selectedPokemon.value);
    form.value.fill(item?.source);
    searchNumber.value = 0;
    searchText.value = undefined;
  }
}

defineEmits<{
  (e: "updated"): void;
}>();
</script>

<template>
  <h2>Edit Roster</h2>
  <h3>Source Pokémon</h3>
  <div class="row">
    <div class="col col-xl-6 col-xxl-4">
      <RosterItem :pokemon="item.source" />
    </div>
  </div>
  <h3>Destination Pokémon</h3>
  <h4>Search</h4>
  <div class="row">
    <div class="col">
      <SearchNumber v-model="searchNumber" />
    </div>
    <div class="col">
      <SearchText v-model="searchText" />
    </div>
  </div>
  <SearchResultSelect :items="roster.items" :search-number="searchNumber" :search-text="searchText" v-model="selectedPokemon" @selected="onSelected" />
  <h4>Properties</h4>
  <RosterItemForm ref="form" :species-id="item.speciesId" @saved="$emit('updated')" />
</template>
