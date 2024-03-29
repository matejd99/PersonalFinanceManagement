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

export interface ValidationproblemErrors { 
    /**
     * Name of input element (field or parameter) that is in invalid. If missing or null it is interpreted that validation error refers to entire request rather than to specific element.
     */
    tag: string;
    /**
     * Unique literal that identifies kind of validation error
     */
    error: ValidationproblemErrors.ErrorEnum;
    /**
     * Message that explains failed validation. To support translation message may embed variable parameters in curly brackets.
     */
    message: string;
}
export namespace ValidationproblemErrors {
    export type ErrorEnum = 'min-length' | 'max-length' | 'required' | 'out-of-range' | 'invalid-format' | 'unknown-enum' | 'not-on-list' | 'check-digit-invalid' | 'combination-required' | 'read-only';
    export const ErrorEnum = {
        MinLength: 'min-length' as ErrorEnum,
        MaxLength: 'max-length' as ErrorEnum,
        Required: 'required' as ErrorEnum,
        OutOfRange: 'out-of-range' as ErrorEnum,
        InvalidFormat: 'invalid-format' as ErrorEnum,
        UnknownEnum: 'unknown-enum' as ErrorEnum,
        NotOnList: 'not-on-list' as ErrorEnum,
        CheckDigitInvalid: 'check-digit-invalid' as ErrorEnum,
        CombinationRequired: 'combination-required' as ErrorEnum,
        ReadOnly: 'read-only' as ErrorEnum
    };
}