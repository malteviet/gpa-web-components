<script lang="ts" module>
	import type { Shape, ShapeId } from '../routes/product-view/types';

	export interface Props {
		imgSrc: string;
		shapes: Shape[];
		onSelect: (shapeId: string) => void;
	}
</script>

<script lang="ts">
	const { imgSrc, shapes, onSelect }: Props = $props();

	let width = $state(0);
	let height = $state(0);

	function handleKeyDown(e: KeyboardEvent, shapeId: ShapeId) {
		if (e.key === ' ' || e.key === 'Enter') {
			e.preventDefault();
			onSelect(shapeId);
		}
	}
</script>

<figure>
	<img src={imgSrc} alt="" bind:clientWidth={width} bind:clientHeight={height} />
	<figcaption>
		<svg class="overlay" {width} {height}>
			{#each shapes as shape}
				{#if shape.type === 'rect'}
					<rect
						class="shape"
						x={`${shape.x}%`}
						y={`${shape.y}%`}
						width={`${shape.width}%`}
						height={`${shape.height}%`}
						role="button"
						tabindex="0"
						onkeydown={(e) => handleKeyDown(e, shape.id)}
						onclick={() => onSelect(shape.id)}
					/>
				{:else if shape.type === 'circle'}
					<circle
						class="shape"
						cx={`${shape.x}%`}
						cy={`${shape.y}%`}
						r={`${shape.radius}%`}
						role="button"
						tabindex="0"
						onkeydown={(e) => handleKeyDown(e, shape.id)}
						onclick={() => onSelect(shape.id)}
					/>
				{/if}
			{/each}
		</svg>
	</figcaption>
</figure>

<style>
	figure {
		position: relative;
		margin: 0;
		height: 100%;
		width: 100%;
		display: flex;
		justify-content: center;
		align-items: center;
	}

	img {
		max-width: 100%;
		max-height: 100%;
	}

	figcaption {
		position: absolute;
		top: 0;
		left: 0;
		width: 100%;
		height: 100%;
		display: flex;
		justify-content: center;
		align-items: center;
	}

	svg .shape {
		cursor: pointer;
		outline: none;
		fill: transparent;
		stroke: #e99700aa;
		stroke-width: 2px;

		&:focus-visible,
		&:hover {
			fill: #e99700aa;
			stroke: #e99700;
		}
	}
</style>
