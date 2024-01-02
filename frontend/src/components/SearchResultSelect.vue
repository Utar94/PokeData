<script setup lang="ts">
import { TarButton, parsingUtils } from "logitar-vue3-ui";
import { computed } from "vue";

import type { RosterInfo, RosterItem } from "@/types/roster";

const props = defineProps<{
  items: RosterItem[];
  modelValue?: number;
  searchNumber?: number;
  searchText?: string;
}>();

const results = computed<RosterInfo[]>(() =>
  props.items
    .map((item) => item.source)
    .filter((pokemon) => {
      if (props.searchNumber && pokemon.number !== props.searchNumber) {
        return false;
      }

      const terms: string[] = props.searchText?.split(" ") ?? [];
      if (terms.length) {
        for (const term of terms) {
          const pattern = new RegExp(term, "i");
          if (
            !pattern.test(pokemon.name) &&
            (!pokemon.category || !pattern.test(pokemon.category)) &&
            !pattern.test(pokemon.region) &&
            !pattern.test(pokemon.primaryType) &&
            (!pokemon.secondaryType || !pattern.test(pokemon.secondaryType))
          ) {
            return false;
          }
        }
      }

      return true;
    }),
);

defineEmits<{
  (e: "selected"): void;
  (e: "update:model-value", value?: number): void;
}>();
</script>

<template>
  <div class="mb-3">
    <div class="input-group">
      <div class="form-floating">
        <select
          aria-describedby="select-search-result"
          aria-label="Destination Pokémon"
          class="form-select"
          :disabled="results.length === 0"
          id="search-results"
          :value="modelValue"
          @input="$emit('update:model-value', parsingUtils.parseNumber(($event.target as HTMLSelectElement).value))"
        >
          <option value="">Select a Pokémon</option>
          <option v-for="result in results" :key="result.number" :value="result.number">
            {{ `#${result.number.toString().padStart(4, "0")} - ${result.name}` }}
          </option>
        </select>
        <label for="search-results">Results</label>
      </div>
      <TarButton :disabled="!modelValue" :icon="['fas', 'check']" id="select-search-result" text="Select" @click="$emit('selected')" />
    </div>
  </div>
</template>
