export interface BaseCefApi {
	ready: () => void;
}

export function getCefApi<T extends BaseCefApi>(): T {
	const proxy = new Proxy<T>({} as T, {
		get: function () {
			if ('cefApi' in window) {
				return window.cefApi as T;
			}
			throw new Error('window.cefApi is undefined');
		}
	});
	return proxy;
}

export function createComponentApi<T>(componentApi: T) {
	return {
		bindToWindow: () => Object.assign(window, { componentApi })
	};
}
