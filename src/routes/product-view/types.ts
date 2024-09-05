import type { BaseCefApi } from '$lib/api';
import type { products } from './products';

export interface Product {
	imgSrc: string;
	shapes: Shape[];
}

export interface BaseShape<T extends string> {
	type: T;
	id: ShapeId;
	x: number;
	y: number;
}

export interface Rect extends BaseShape<'rect'> {
	width: number;
	height: number;
}

export interface Circle extends BaseShape<'circle'> {
	radius: number;
}

export type Shape = Rect | Circle;

export type ShapeId = string;

export type ProductId = keyof typeof products;

// API

export interface CefApi extends BaseCefApi {
	onSelect: (shapeId: ShapeId) => void;
}

export interface ComponentApi {
	loadProduct: (productId: ProductId) => void;
}
