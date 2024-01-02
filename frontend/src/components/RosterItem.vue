<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { TarBadge, TarImage } from "logitar-vue3-ui";

import type { RosterInfo } from "@/types/roster";

defineProps<{
  pokemon: RosterInfo;
}>();
</script>

<template>
  <div class="row">
    <div class="col">
      <a :href="`https://bulbapedia.bulbagarden.net/wiki/${pokemon.name}_(Pok%C3%A9mon)`" target="_blank">
        {{ `#${pokemon.number.toString().padStart(4, "0")} - ${pokemon.name}` }}
        <FontAwesomeIcon :icon="['fas', 'arrow-up-right-from-square']" />
      </a>
    </div>
    <div class="col text-end">
      <TarImage :src="`/img/types/${pokemon.primaryType.toLowerCase()}.png`" :alt="pokemon.primaryType" height="16" />
      <TarImage
        v-if="pokemon.secondaryType"
        class="ms-1"
        :src="`/img/types/${pokemon.secondaryType.toLowerCase()}.png`"
        :alt="pokemon.secondaryType"
        height="16"
      />
    </div>
  </div>
  <div class="row">
    <div class="col">
      {{ pokemon.category ?? "-" }}
    </div>
    <div class="col text-end">
      <TarBadge v-if="pokemon.region" variant="dark">{{ pokemon.region }}</TarBadge>
      <template v-else>{{ "-" }}</template>
    </div>
  </div>
  <div>
    <TarBadge class="me-2" v-if="pokemon.isBaby">🐣 Baby</TarBadge>
    <TarBadge class="me-2" v-if="pokemon.isLegendary">🐉 Legendary</TarBadge>
    <TarBadge class="me-2" v-if="pokemon.isMythical">🌠 Mythical</TarBadge>
    <span v-if="!pokemon.isBaby && !pokemon.isLegendary && !pokemon.isMythical">-</span>
  </div>
</template>
