<script setup lang="ts">
import { TarButton, TarModal } from "logitar-vue3-ui";
import { ref } from "vue";

import RosterItem from "./RosterItem.vue";
import type { RosterItem as RosterItemType, SavedRosterItem } from "@/types/roster";
import { removeRosterItem } from "@/api/roster";

const props = defineProps<{
  item?: RosterItemType;
}>();

const loading = ref<boolean>(false);
const modal = ref<InstanceType<typeof TarModal>>();

const emit = defineEmits<{
  (e: "removed", item: SavedRosterItem): void;
}>();

async function onRemove(): Promise<void> {
  if (!loading.value && props.item) {
    loading.value = true;
    try {
      const removed = await removeRosterItem(props.item.speciesId);
      emit("removed", removed);
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
</script>

<template>
  <TarModal ref="modal" title="Remove Pokémon">
    <template v-if="item?.destination">
      <p>Do you really want to remove the following Pokémon from the roster?</p>
      <RosterItem :pokemon="item.destination" />
    </template>
    <template #footer>
      <TarButton :icon="['fas', 'ban']" text="Cancel" variant="secondary" @click="modal.hide()" />
      <TarButton :disabled="loading" :icon="['fas', 'trash-can']" :loading="loading" text="Remove" variant="danger" @click="onRemove" />
    </template>
  </TarModal>
</template>
