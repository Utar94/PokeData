<script setup lang="ts">
import { TarButton, parsingUtils } from "logitar-vue3-ui";

defineProps<{
  modelValue: number;
}>();

defineEmits<{
  (e: "update:model-value", value: number): void;
}>();
</script>

<template>
  <div class="mb-3">
    <div class="input-group">
      <div class="form-floating">
        <input
          aria-describedby="clear-search-number"
          class="form-control"
          id="search-number"
          min="1"
          max="1025"
          placeholder="Number"
          type="number"
          :value="modelValue"
          @input="$emit('update:model-value', parsingUtils.parseNumber(($event.target as HTMLInputElement).value) ?? 0)"
        />
        <label for="search-number">Number</label>
      </div>
      <TarButton :icon="['fas', 'times']" id="clear-search-number" variant="danger" @click="$emit('update:model-value', 0)" />
    </div>
  </div>
</template>
