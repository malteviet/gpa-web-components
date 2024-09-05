import type { Product } from './types';
import notFound from '/src/assets/product-view/notFound.png';
import kipp2Fach from '/src/assets/product-view/kipp2Fach.png';

export const products = Object.freeze({
	notFound: {
		imgSrc: notFound,
		shapes: [
			{ type: 'rect', id: 'off', x: 25, y: 25, width: 50, height: 25 },
			{ type: 'rect', id: 'on', x: 25, y: 50, width: 50, height: 25 },
			{ type: 'circle', id: 'top-left', x: 38, y: 22, radius: 5 },
			{ type: 'circle', id: 'top-right', x: 62, y: 22, radius: 5 },
			{ type: 'circle', id: 'bottom-left', x: 38, y: 78, radius: 5 },
			{ type: 'circle', id: 'bottom-right', x: 62, y: 78, radius: 5 }
		]
	},
	kipp2Fach: {
		imgSrc: kipp2Fach,
		shapes: [
			{ type: 'rect', id: 'left-up', x: 0, y: 0, width: 50, height: 50 },
			{ type: 'rect', id: 'right-up', x: 0, y: 50, width: 50, height: 50 },
			{ type: 'rect', id: 'left-down', x: 50, y: 0, width: 50, height: 50 },
			{ type: 'rect', id: 'right-down', x: 50, y: 50, width: 50, height: 50 },
			{ type: 'circle', id: 'center', x: 50, y: 50, radius: 5 }
		]
	}
} satisfies Record<string, Product>);
