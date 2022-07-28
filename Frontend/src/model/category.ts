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

/**
 * category
 */
export interface Category { 
    /**
     * Code of category
     */
    readonly code: string;
    /**
     * Name of the category
     */
    name: string;
    /**
     * Parent code if subcategory
     */
    parentCode?: string;
}