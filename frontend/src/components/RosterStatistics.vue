<script setup lang="ts">
import { TarImage } from "logitar-vue3-ui";

import type { RosterStatistics } from "@/types/roster";

defineProps<{
  statistics: RosterStatistics;
}>();
</script>

<template>
  <div>
    <p>Selected Pokémon: {{ statistics.count }}</p>
    <div class="row">
      <div class="col">
        <h2>Types</h2>
        <table class="table table-striped">
          <thead>
            <tr>
              <th scope="col">Name</th>
              <th scope="col">Count</th>
              <th scope="col">Percentage</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="pokemonType in statistics.types" :key="pokemonType.key">
              <td>
                <TarImage :src="`/img/types/${pokemonType.key.toLowerCase()}.png`" :alt="pokemonType.key" height="24" />
              </td>
              <td>{{ pokemonType.count }}</td>
              <td>{{ (pokemonType.percentage * 100).toFixed(2) }}%</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="col">
        <h2>Regions</h2>
        <table class="table table-striped">
          <thead>
            <tr>
              <th scope="col">Name</th>
              <th scope="col">Count</th>
              <th scope="col">Percentage</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="region in statistics.regions" :key="region.key">
              <td>{{ region.key }}</td>
              <td>{{ region.count }}</td>
              <td>{{ (region.percentage * 100).toFixed(2) }}%</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>
