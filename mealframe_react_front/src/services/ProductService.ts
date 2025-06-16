import { EntityService } from "./EntityService";
import {IProduct} from "@/types/domain/IProduct";
import {IProductAdd} from "@/types/domain/IProductAdd";

export class ProductService extends EntityService<IProduct, IProductAdd> {
    constructor(){
        super('products')
    }
}