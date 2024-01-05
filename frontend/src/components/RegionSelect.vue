<script setup lang="ts">
import { TarSelect, type SelectOption, type SelectSize } from "logitar-vue3-ui";
import { computed, onMounted, ref } from "vue";

import type { Region } from "@/types/region";
import { searchRegions } from "@/api/region";

withDefaults(
  defineProps<{
    disabled?: boolean;
    floating?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    size?: SelectSize;
  }>(),
  {
    floating: true,
    id: "region",
    label: "Region",
    placeholder: "Select a region",
  },
);

const regions = ref<Region[]>([]);

const options = computed<SelectOption[]>(() => regions.value.map(({ displayName, uniqueName }) => ({ text: displayName, value: uniqueName })));

onMounted(async () => (regions.value = (await searchRegions({ sort: [{ field: "Number", isDescending: false }] })).items));

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
