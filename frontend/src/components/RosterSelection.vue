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
        <th scope="col"></th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in roster.items" :key="item.speciesId">
        <td>
          <RosterItem :pokemon="item.source" />
        </td>
        <td>
          <RosterItem v-if="item.destination" :pokemon="item.destination" />
        </td>
        <td>
          <div class="text-end">
            <TarButton v-if="!item.destination" :icon="['fas', 'plus']" text="Add" variant="success" @click="$emit('selected', item)" />
            <template v-else>
              <TarButton class="me-2" disabled :icon="['fas', 'pen-to-square']" text="Edit" variant="primary" @click="$emit('selected', item)" />
              <TarButton disabled :icon="['fas', 'times']" text="Remove" variant="danger" />
              <!-- TODO(fpion): complete Edit -->
              <!-- TODO(fpion): complete Remove -->
            </template>
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</template>
