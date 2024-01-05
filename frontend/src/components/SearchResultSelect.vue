<script setup lang="ts">
import { TarButton, TarSelect, parsingUtils, type SelectOption, type SelectSize } from "logitar-vue3-ui";
import { computed } from "vue";

import type { RosterItem } from "@/types/roster";

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    floating?: boolean;
    id?: string;
    items: RosterItem[];
    label?: string;
    modelValue?: number;
    placeholder?: string;
    required?: boolean;
    searchNumber?: number;
    searchText?: string;
    size?: SelectSize;
  }>(),
  {
    floating: true,
    id: "search-results",
    label: "Results",
    placeholder: "Select a Pokémon",
  },
);

const options = computed<SelectOption[]>(() =>
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
    })
    .slice(0, 19 + 1)
    .map(({ name, number }) => ({ text: `#${number.toString().padStart(4, "0")} - ${name}`, value: number.toString() })),
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
        <TarSelect
          aria-label="Destination Pokémon"
          described-by="select-search-result"
          :disabled="disabled"
          :floating="floating"
          :id="id"
          :label="label"
          :model-value="modelValue"
          :options="options"
          :placeholder="placeholder"
          :required="required"
          :size="size"
          @update:model-value="$emit('update:model-value', parsingUtils.parseNumber($event))"
        >
          <template #append>
            <TarButton :disabled="!modelValue" :icon="['fas', 'check']" id="select-search-result" text="Select" @click="$emit('selected')" />
          </template>
        </TarSelect>
      </div>
    </div>
  </div>
</template>
