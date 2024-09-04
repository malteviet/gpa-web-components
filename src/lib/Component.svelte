<script lang="ts">
	interface dragableObject {
		name: string;
		id: string;
	}
	const items: dragableObject[] = [
		{ name: 'Item1', id: 'hallo' },
		{ name: 'Item2', id: 'du' },
		{ name: 'Item3', id: 'wir' },
		{ name: 'Item4', id: 'sie' }
	];

	function handleDrag(event: DragEvent, item: dragableObject) {
		console.log(item.id);
		event.dataTransfer?.setData('text', item.id);
		const element: Element = document.getElementById('adorner') as Element;
		element.innerHTML = item.name;
		event.dataTransfer?.setDragImage(element, 0, 0);
	}
</script>

<p id="adorner"></p>
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
</style>
