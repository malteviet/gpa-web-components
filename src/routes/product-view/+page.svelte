<script lang="ts">
	import ProductView from '$lib/ProductView.svelte';
	import { getCefApi, createComponentApi } from '../../lib/api';
	import { products } from './products';
	import type { CefApi, ComponentApi, ProductId } from './types';

	const cefApi = getCefApi<CefApi>();

	const componentApi = createComponentApi<ComponentApi>({
		loadProduct: (productId: ProductId) => {
			selectedProduct = products[productId] ?? products.notFound;
		}
	});

	$effect(() => {
		componentApi.bindToWindow();
		cefApi.ready();
	});

	let selectedProduct = $state(products.notFound);
</script>

<div>
	<ProductView {...selectedProduct} onSelect={(shapeId) => cefApi.onSelect(shapeId)} />
</div>

<style>
	div {
		padding: 1rem;
		height: 100vh;
		width: 100vw;
		box-sizing: border-box;
	}
</style>
