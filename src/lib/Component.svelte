<script lang="ts">
	import browserImage from '../assets/browser-svgrepo-com.svg';
	import cameraImage from '../assets/film-camera-svgrepo-com.svg';
	import photoImage from '../assets/photo-camera-svgrepo-com.svg';
	import speakerImage from '../assets/speaker-svgrepo-com.svg';
	import htmlImage from '../assets/html-16-svgrepo-com.svg';

	interface DraggableObject {
		name: string;
		id: string;
		urn: string;
		image: string;
	}

	interface CatalogEntry {
		id: string;
		name: string;
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
		// {
		// 	name: 'call display module',
		// 	id: 'GIDS01DCIP-1.0.934.0-10',
		// 	urn: 'de.gira.schema.components.DcsIp.CallDisplayModule',
		// 	image: browserImage
		// },
		// {
		// 	name: 'call button single',
		// 	id: 'GIDS01DCIP-1.0.934.0-10',
		// 	urn: 'de.gira.schema.components.DcsIp.CallButtonModuleSingle',
		// 	image: cameraImage
		// },
		// {
		// 	name: 'call button double',
		// 	id: 'GIDS01DCIP-1.0.934.0-10',
		// 	urn: 'de.gira.schema.components.DcsIp.CallButtonModuleDouble',
		// 	image: photoImage
		// },
		// {
		// 	name: 'camera module',
		// 	id: 'GIDS01DCIP-1.0.934.0-10',
		// 	urn: 'de.gira.schema.components.DcsIp.CameraModule',
		// 	image: speakerImage
		// }
	];

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
	
	let items = $state<CatalogEntry[]>(fallbackItems);
	let draggedItem = $state<CatalogEntry>();
	let adornerComponent = $state<Element>();

	/** This method should be called from C#. */
	export function setEntries(json: string) {
		console.log("setEntries called");
		items = createDraggableObjects(json);
	}
	
	function handleDrag(event: DragEvent, item: DraggableObject) {
		draggedItem = item;
		console.log(item.id);

		event.dataTransfer?.setData('text/plain', item.urn);
		event.dataTransfer?.setDragImage(adornerComponent!, 0, 0);
	}

	let selectedItem = $state<DraggableObject>();

	function onFocusHandler(item: DraggableObject) {
		selectedItem = item;
	}
</script>

<div>
	<p bind:this={adornerComponent}>{draggedItem?.name ?? ''}</p>
</div>
<ul>
	{#each items as item (item.urn)}
		<li id={item.id} draggable="true" ondragstart={(event) => handleDrag(event, item)}>
			<button onfocus={() => onFocusHandler(item)} class:selected={item.urn === selectedItem?.urn}>
				<img src={item.image} alt={item.name} />
				{item.name}
			</button>
		</li>
	{/each}
</ul>

<style>
	:root {
		font-family: Arial, Helvetica, sans-serif;
	}

	li > button {
		all: unset;
		padding: 2px;
		width: 100%;
		height: 100%;
	}

	button.selected {
		background-color: lightgray;
	}

	img {
		width: 16px;
		height: 16px;
	}

	ul {
		border: 1px darkgray solid;
		padding: 0px;
		margin: 0px;
		width: 220px;
	}

	ul > li {
		box-sizing: border-box;
		display: flex;
		list-style-type: none;
		background-color: white;
		border-bottom: 1px darkgray dotted;
		width: 100%;
		cursor: pointer;
		align-items: center;
		gap: 10px;
	}

	ul > li:hover,
	ul > li > button:hover {
		color: #bace00;
		background-color: white;
	}

	ul > li:hover,
	ul > li > button:focus-within {
		color: black;
		background-color: #bace00;
	}

	div > p {
		position: absolute;
		left: -100%;
		color: lightgray;
		border: lightgray solid 1px;
		border-radius: 5px;
		background-color: white;
		opacity: 0.01;
		padding: 2px;
	}
</style>