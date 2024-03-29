/**
 * Personal Finance Management API
 * Personal Finance Management API allows analyze of a client's spending patterns against pre-defined budgets over time 
 *
 * OpenAPI spec version: v1
 * Contact: aleksandar.milosevic@asseco-see.rs
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { ValidationproblemErrors } from './validationproblemErrors';

/**
 * Details of failed validation
 */
export interface ValidationProblem { 
    /**
     * List of validation errors
     */
    errors: Array<ValidationproblemErrors>;
}