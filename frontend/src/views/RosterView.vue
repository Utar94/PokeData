<script setup lang="ts">
import { onMounted, ref } from "vue";
import { TarTab, TarTabs } from "logitar-vue3-ui";

import RosterEdit from "@/components/RosterEdit.vue";
import RosterSelection from "@/components/RosterSelection.vue";
import RosterStatistics from "@/components/RosterStatistics.vue";
import type { PokemonRoster, RosterItem } from "@/types/roster";
import { readRoster } from "@/api/roster";

const roster = ref<PokemonRoster>();
const source = ref<RosterItem>();

async function refresh(): Promise<void> {
  roster.value = await readRoster();
}

onMounted(refresh);
</script>

<template>
  <main class="container-fluid">
    <h1>Roster</h1>
    <TarTabs v-if="roster">
      <TarTab active title="Selection">
        <RosterSelection :roster="roster" @selected="source = $event" />
      </TarTab>
      <TarTab :disabled="!source" title="Edit">
        <RosterEdit v-if="source && roster" :item="source" :roster="roster" @updated="refresh" />
      </TarTab>
      <TarTab title="Statistics">
        <RosterStatistics :roster="roster" />
      </TarTab>
    </TarTabs>
  </main>
</template>
