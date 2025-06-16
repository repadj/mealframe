export interface IShoppingList {
    id: string;
    sListName: string;
    shoppingItems?: IShoppingItem[];
}

export interface IShoppingItem {
    id: string;
    amount: number;
    unit: string;
    productId: string;
    productName: string;
    productCategory: string;
}