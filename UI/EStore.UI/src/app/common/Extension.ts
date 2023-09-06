export class ObjectUtil {
    static isNullOrWhiteSpace(value: string | null): boolean {
        if (value === null || value === undefined || value.trim() === '') {
            return true;
        }
        return false;
    }

    static isNullOrUndefined(obj: any) {
        return typeof obj === "undefined" || obj === null;
    }
    static isNullOrUndefinedOrEmpty(obj: any) {
        return typeof obj === "undefined" || obj === null || obj === "";
    }
}