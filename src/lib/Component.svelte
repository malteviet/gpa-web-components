<script lang="ts">
	import browserImage from '../assets/browser-svgrepo-com.svg';
	import cameraImage from '../assets/film-camera-svgrepo-com.svg';
	import photoImage from '../assets/photo-camera-svgrepo-com.svg';
	import speakerImage from '../assets/speaker-svgrepo-com.svg';

	interface draggableObject {
		name: string;
		id: string;
		urn: string;
		image: string;
	}
	const items: draggableObject[] = [
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

	let draggedItem = $state<draggableObject>();
	let adornerComponent = $state<Element>();

	function handleDrag(event: DragEvent, item: draggableObject) {
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
			<img src={item.image} alt={item.name} />
			{item.name}
		</li>
	{/each}
</ul>

<style>
	:root {
		font-family: Arial, Helvetica, sans-serif;
	}

	img {
		width: 16px;
		height: 16px;
		margin: 4px 0px 0px 2px;
	}

	ul > li {
		list-style-type: none;
		background-color: lightgray;
		border: 1px darkgray solid;
		margin: 2px;
		width: 200px;
		cursor: pointer;
	}

	div > p {
		position: absolute;
		left: -100%;
		background-color: violet;
	}
</style>
