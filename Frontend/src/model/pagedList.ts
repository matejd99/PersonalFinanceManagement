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
import { SortOrderEnum } from './sortOrderEnum';

/**
 * List with support for paging
 */
export interface PagedList { 
    /**
     * Total number of items in collection
     */
    totalCount?: number;
    /**
     * Size of the page
     */
    pageSize?: number;
    /**
     * Index of current page
     */
    page?: number;
    /**
     * Total number of pages of set size
     */
    totalPages?: number;
    sortOrder?: SortOrderEnum;
    /**
     * Attribute of the collection item to sort by
     */
    sortBy?: string;
}