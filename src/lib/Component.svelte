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
			<button>
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

	button {
		all: unset;
		padding: 2px;
		width: 100%;
		height: 100%;
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
		background-color: violet;
	}
</style>
