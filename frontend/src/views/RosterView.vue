<script setup lang="ts">
import { onMounted, ref } from "vue";
import { TarButton, TarTab, TarTabs } from "logitar-vue3-ui";

import RosterEdit from "@/components/RosterEdit.vue";
import RosterSelection from "@/components/RosterSelection.vue";
import RosterStatistics from "@/components/RosterStatistics.vue";
import type { PokemonRoster, RosterItem, SavedRosterItem } from "@/types/roster";
import { readRoster } from "@/api/roster";

const loading = ref<boolean>(false);
const roster = ref<PokemonRoster>();
const source = ref<RosterItem>();

async function refresh(): Promise<void> {
  if (!loading.value) {
    loading.value = true;
    try {
      roster.value = await readRoster();
      loading.value = false;
    } catch (e: unknown) {
      loading.value = false;
      throw e;
    }
  }
}

function onSave(saved: SavedRosterItem): void {
  if (roster.value) {
    const index = roster.value.items.findIndex((item) => item.speciesId === source.value?.speciesId);
    if (index >= 0) {
      roster.value.items.splice(index, 1, saved.item);
    }
    roster.value.stats = saved.stats;
  }
}

onMounted(refresh);
</script>

<template>
  <main class="container-fluid">
    <h1>Roster</h1>
    <div class="mb-3">
      <TarButton :disabled="loading" :icon="['fas', 'arrows-rotate']" :loading="loading" text="Refresh" @click="refresh" />
    </div>
    <TarTabs v-if="roster">
      <TarTab active title="Selection">
        <RosterSelection :items="roster.items" @selected="source = $event" />
      </TarTab>
      <TarTab :disabled="!source" title="Edit">
        <RosterEdit v-if="source && roster" :item="source" :roster="roster" @saved="onSave" />
      </TarTab>
      <TarTab title="Statistics">
        <RosterStatistics :statistics="roster.stats" />
      </TarTab>
    </TarTabs>
  </main>
</template>
