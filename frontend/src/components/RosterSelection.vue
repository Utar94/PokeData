<script setup lang="ts">
import { TarButton } from "logitar-vue3-ui";

import RosterItem from "./RosterItem.vue";
import type { PokemonRoster, RosterItem as RosterItemType } from "@/types/roster";

defineProps<{
  roster: PokemonRoster;
}>();

defineEmits<{
  (e: "selected", pokemon: RosterItemType): void;
}>();
</script>

<template>
  <table class="table table-striped">
    <thead>
      <tr>
        <th scope="col">Source</th>
        <th scope="col">Destination</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in roster.items" :key="item.speciesId">
        <td>
          <RosterItem :pokemon="item.source" />
        </td>
        <td>
          <!-- TODO(fpion): Edit / Remove existing destination Pokémon -->
          <RosterItem v-if="item.destination" :pokemon="item.destination" />
          <div v-else class="text-end">
            <TarButton :icon="['fas', 'plus']" text="Add" variant="success" @click="$emit('selected', item)" />
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</template>
