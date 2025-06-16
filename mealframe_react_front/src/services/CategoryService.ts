import {EntityService} from "@/services/EntityService";
import {ICategory} from "@/types/domain/ICategory";
import {ICategoryAdd} from "@/types/domain/ICategoryAdd";

export class CategoryService extends EntityService<ICategory, ICategoryAdd> {
    constructor(){
        super('categories')
    }
}