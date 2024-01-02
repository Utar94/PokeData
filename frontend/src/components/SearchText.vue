<script setup lang="ts">
import { TarButton } from "logitar-vue3-ui";

defineProps<{
  modelValue?: string;
}>();

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <div class="mb-3">
    <div class="input-group">
      <div class="form-floating">
        <input
          aria-describedby="clear-search-text"
          class="form-control"
          id="search-text"
          placeholder="Search"
          type="search"
          :value="modelValue"
          @input="$emit('update:model-value', ($event.target as HTMLInputElement).value ?? undefined)"
        />
        <label for="search-text">Search</label>
      </div>
      <TarButton :icon="['fas', 'times']" id="clear-search-text" variant="danger" @click="$emit('update:model-value', undefined)" />
    </div>
  </div>
</template>

<style scoped>
input[type="search"]::-webkit-search-decoration,
input[type="search"]::-webkit-search-cancel-button,
input[type="search"]::-webkit-search-results-button,
input[type="search"]::-webkit-search-results-decoration {
  -webkit-appearance: none;
}
</style>
