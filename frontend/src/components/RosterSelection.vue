<script setup lang="ts">
import { TarButton } from "logitar-vue3-ui";

import RosterItem from "./RosterItem.vue";
import type { RosterItem as RosterItemType } from "@/types/roster";

defineProps<{
  items: RosterItemType[];
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
        <th scope="col">
          <div class="text-end">Actions</div>
        </th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in items" :key="item.speciesId">
        <td>
          <RosterItem :pokemon="item.source" />
        </td>
        <td>
          <RosterItem v-if="item.destination" :pokemon="item.destination" />
          <template v-else>N/A</template>
        </td>
        <td>
          <div class="text-end">
            <template v-if="item.destination">
              <TarButton class="me-2" :icon="['fas', 'pen-to-square']" text="Edit" variant="primary" @click="$emit('selected', item)" />
              <TarButton disabled :icon="['fas', 'times']" text="Remove" variant="danger" />
              <!-- TODO(fpion): complete Remove -->
            </template>
            <TarButton v-else :icon="['fas', 'plus']" text="Add" variant="success" @click="$emit('selected', item)" />
          </div>
        </td>
      </tr>
    </tbody>
  </table>
</template>
