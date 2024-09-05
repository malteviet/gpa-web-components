<script lang="ts" module>
	/*
	declare global {
		interface Window {
			component?: Component;
		}
	}
  */
</script>

<script lang="ts">
	// images
	import browserImage from './assets/browser-svgrepo-com.svg';
	import cameraImage from './assets/film-camera-svgrepo-com.svg';
	import photoImage from './assets/photo-camera-svgrepo-com.svg';
	import speakerImage from './assets/speaker-svgrepo-com.svg';
	import htmlImage from './assets/html-16-svgrepo-com.svg';
	// component
	import Component, { type DraggableObject } from '$lib/Component.svelte';

	interface CatalogEntry {
		name: string;
		id: string;
		urn: string;
		image: string;
	}

	const imageDictionary = new Map<string, string>([
		['browserImage', browserImage],
		['cameraImage', cameraImage],
		['photoImage', photoImage],
		['speakerImage', speakerImage]
	]);

	const fallbackItems: DraggableObject[] = [
		{
			name: 'call display module',
			id: 'GIDS01DCIP-1.0.934.0-10',
			urn: 'de.gira.schema.components.DcsIp.CallDisplayModule',
			image: browserImage
		},
		{
			name: 'call button single',
			id: 'GIDS01DCIP-1.0.934.0-10',
			urn: 'de.gira.schema.components.DcsIp.CallButtonModuleSingle',
			image: cameraImage
		},
		{
			name: 'call button double',
			id: 'GIDS01DCIP-1.0.934.0-10',
			urn: 'de.gira.schema.components.DcsIp.CallButtonModuleDouble',
			image: photoImage
		},
		{
			name: 'camera module',
			id: 'GIDS01DCIP-1.0.934.0-10',
			urn: 'de.gira.schema.components.DcsIp.CameraModule',
			image: speakerImage
		}
	];

	function createObjectsFromJson(json: string): DraggableObject[] {
		return createDraggableObjects(JSON.parse(json) as CatalogEntry[]);
	}

	function createDraggableObjects(catalogentries: CatalogEntry[]): DraggableObject[] {
		const result: DraggableObject[] = [];
		for (const catalogEntry of catalogentries) {
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

	let component = $state<Component>();

	$effect(() => {
		Object.assign(window, { component });
	});

	/** This method should be called from C#. */
	function setEntries(json: string) {
		if (component instanceof Component) {
			component.items = createObjectsFromJson(json);
		} else {
			console.log('The device library component is not initialized yet.');
		}
	}
</script>

<Component bind:this={component} items={fallbackItems} />
