export const currencyFormat = (value) => {
    let formatValue = value ? parseFloat(value).toFixed(2) : value
    formatValue = formatValue ? formatValue.replace('.', ',') : formatValue
    return formatValue
}

