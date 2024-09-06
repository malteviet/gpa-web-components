<script lang="ts">
	import Component, { type CatalogEntry, type DraggableObject } from '$lib/Component.svelte';
	import { imageDictionary } from '$lib/device-items';
	import htmlImage from '/src/assets/html-16-svgrepo-com.svg';

	let component = $state<Component>();

	$effect(() => {
		Object.assign(window, { component: { setEntries } });
	});

	let items = $state<CatalogEntry[]>([]);

	/**
	 * This method should be called from C#.
	 *
	 * @example window.component.setEntries('[ { "name": "call display module", "id": "GIDS01DCIP-1.0.934.0-10", "urn": "de.gira.schema.components.DcsIp.CallDisplayModule", "image": "dsfdsff" }]')
	 */
	function setEntries(json: string) {
		console.log('setEntries called');
		items = createDraggableObjects(json);
	}

	function createDraggableObjects(json: string): DraggableObject[] {
		const result: DraggableObject[] = [];
		for (const catalogEntry of JSON.parse(json) as CatalogEntry[]) {
			const image: string = imageDictionary.has(catalogEntry.image)
				? (imageDictionary.get(catalogEntry.image) as string)
				: htmlImage;
			result.push({
				name: catalogEntry.name,
				id: catalogEntry.id,
				urn: catalogEntry.urn,
				image: image
			});
		}

		return result;
	}
</script>

<Component bind:this={component} {items} />
