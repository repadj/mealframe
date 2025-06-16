
import { EntityService } from "./EntityService";
import {INutritionGoal} from "@/types/domain/INutritionGoal";
import {INutritionGoalAdd} from "@/types/domain/INutritionGoalAdd";

export class NutritionalGoalService extends EntityService<INutritionGoal, INutritionGoalAdd> {
    constructor(){
        super('nutritionalGoals')
    }
}
