export default function createDateAsUTC(date) {
    return new Date(Date.UTC(date.getFullYear(),
        date.getMonth(),
        date.getDate()));
}