<script lang="ts" module>
	export interface DraggableObject {
		name: string;
		id: string;
		urn: string;
		image: string;
	}

	export interface CatalogEntry {
		id: string;
		name: string;
		urn: string;
		image: string;
	}
</script>

<script lang="ts">
	interface Props {
		items: CatalogEntry[];
	}

	const { items }: Props = $props();

	let draggedItem = $state<CatalogEntry>();
	let adornerComponent = $state<Element>();

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
