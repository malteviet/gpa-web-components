<script lang="ts">
	import Adorner from './Adorner.svelte';

	interface dragableObject {
		name: string;
		id: string;
		urn: string;
	}
	const items: dragableObject[] = [
		{
			name: 'Item 1',
			id: 'GIDS01DCIP-1.0.934.0-10',
			urn: 'de.gira.schema.components.DcsIp.CallDisplayModule'
		},
		{
			name: 'Item 2',
			id: 'GIDS01DCIP-1.0.934.0-10',
			urn: 'de.gira.schema.components.DcsIp.CallButtonModuleSingle'
		},
		{
			name: 'Item 3',
			id: 'GIDS01DCIP-1.0.934.0-10',
			urn: 'de.gira.schema.components.DcsIp.CallButtonModuleDouble'
		},
		{
			name: 'Item 4',
			id: 'GIDS01DCIP-1.0.934.0-10',
			urn: 'de.gira.schema.components.DcsIp.CameraModule'
		}
	];

	let draggedItem = $state<dragableObject>();
	let adornerComponent = $state<Element>();

	function handleDrag(event: DragEvent, item: dragableObject) {
		draggedItem = item;
		console.log(item.id);

		event.dataTransfer?.setData('text/plain', JSON.stringify(item));
		event.dataTransfer?.setDragImage(adornerComponent!, 0, 0);
	}
</script>

<div>
	<p bind:this={adornerComponent}>{draggedItem?.name ?? ''}</p>
</div>
<ul>
	{#each items as item}
		<li id={item.id} draggable="true" ondragstart={(event) => handleDrag(event, item)}>
			{item.name}
		</li>
	{/each}
</ul>

<style>
	ul > li {
		background-color: aqua;
		width: 200px;
		cursor: pointer;
	}

	div > p {
		position: absolute;
		left: 100%;
		background-color: violet;
	}
</style>
