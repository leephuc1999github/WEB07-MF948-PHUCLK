import moment from 'moment'
class Common {
    /**
     * Check value is empty
     * @param {*} value parameter
     * @returns true if value is empty else 
     * Author : LP(28/8)
     */
    static isNullOrUndifined(value) {
        return value === "" || value === null || typeof (value) === 'undefined' ? true : false;
    }


    /**------------------------------------------
     * Format data of birth
     * Author : LP(28/8)
     */
    static formatDateDDMMYYYY(date) {
        if (Common.isNullOrUndifined(date)) return "";
        try {
            return moment(date).format("DD/MM/yyyy");
        } catch (error) {
            console.log("formatDateDDMMYYYY\n" + error);
        }
    }


    /**
     * Validate Email
     * @param {*} email 
     * @returns 
     * Author : LP(29/8)
     */
    static validateEmail(email) {
        const patternEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return patternEmail.test(email);
    }
}
export default Common;