<script setup lang="ts">
import { TarButton, TarInput, parsingUtils, type InputSize } from "logitar-vue3-ui";
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    floating?: boolean;
    id?: string;
    label?: string;
    max?: number;
    min?: number;
    modelValue?: number;
    placeholder?: string;
    required?: boolean;
    size?: InputSize;
  }>(),
  {
    floating: true,
    id: "search-number",
    label: "Number",
    min: 0,
    max: 9999,
    modelValue: 0,
  },
);

const inputPlaceholder = computed<string | undefined>(() => (props.floating ? props.placeholder ?? props.label : props.placeholder));

defineEmits<{
  (e: "update:model-value", value: number): void;
}>();
</script>

<template>
  <TarInput
    aria-describedby="clear-search-number"
    :disabled="disabled"
    :floating="floating"
    :id="id"
    :label="label"
    :max="max"
    :min="min"
    :model-value="modelValue"
    :placeholder="inputPlaceholder"
    :required="required"
    :size="size"
    step="1"
    type="number"
    @update:model-value="$emit('update:model-value', parsingUtils.parseNumber($event) ?? 0)"
  >
    <template #append>
      <TarButton :icon="['fas', 'times']" id="clear-search-number" variant="danger" @click="$emit('update:model-value', 0)" />
    </template>
  </TarInput>
</template>
