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
	import Component from '$lib/Component.svelte';

	let component = $state<Component>();

	$effect(() => {
		Object.assign(window, { component });
	});
</script>

<Component bind:this={component} />
