<script lang="ts">
	interface ComponentPage {
		href: string;
		name: string;
	}

	const pages: ComponentPage[] = [
		{ href: './device-lib', name: 'Device Lib' },
		{ href: './product-view', name: 'Product View' }
	];
</script>

<ul>
	{#each pages as page}
		<li>
			<a href={page.href}>{page.name}</a>
		</li>
	{/each}
</ul>
