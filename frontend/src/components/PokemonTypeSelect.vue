<script setup lang="ts">
import { TarSelect, type SelectOption, type SelectSize } from "logitar-vue3-ui";
import { computed, onMounted, ref } from "vue";

import type { PokemonType } from "@/types/pokemon";
import { searchPokemonTypes } from "@/api/pokemon";

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    exclude?: string[];
    floating?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    size?: SelectSize;
  }>(),
  {
    exclude: () => [],
    floating: true,
    id: "pokemon-type",
    label: "Pokémon Type",
    placeholder: "Select a Pokémon type",
  },
);

const pokemonTypes = ref<PokemonType[]>([]);

const options = computed<SelectOption[]>(() =>
  pokemonTypes.value
    .filter(({ uniqueName }) => !props.exclude.includes(uniqueName))
    .map(({ displayName, uniqueName }) => ({ text: displayName, value: uniqueName })),
);

onMounted(async () => (pokemonTypes.value = (await searchPokemonTypes({ sort: [{ field: "DisplayName", isDescending: false }] })).items));

defineEmits<{
  (e: "update:model-value", value: string): void;
}>();
</script>

<template>
  <TarSelect
    :disabled="disabled"
    :floating="floating"
    :id="id"
    :label="label"
    :model-value="modelValue"
    :options="options"
    :placeholder="placeholder"
    :required="required"
    :size="size"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
