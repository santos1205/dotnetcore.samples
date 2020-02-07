export const totalsProcess = (kit) => {    
    //console.log(kit)
    // TOTAL JEWELS, PARTIAL JEWELS, TOTAL SELL
    let totalJewels = 0
    let partialJewels = 0
    let totalSell = 0
    let partialSell = 0
    kit.jewels.map(function(value, index){        
        totalJewels++        
        totalSell += value.valor
        if(value.isSold == true){
            partialJewels++
            partialSell += value.valor
        }                    
    })  
    kit.totalJewels = totalJewels      
    kit.partialJewels = partialJewels
    kit.totalSell = totalSell
    kit.partialSell = partialSell
    return kit
}

export const updateJewel = (kit, index) => {    
    let jewelUpdate = kit.jewels[index].isSold == false ? true : false
    kit = {
     ...kit, jewels: 
     [
       ...kit.jewels.slice(0, index),  // update array state: antes do registro selecionado
       {
         ...kit.jewels[index],
         isSold: jewelUpdate
       },
       ...kit.jewels.slice(index + 1), // update array state: depois do registro selecionado
     ] 
  }   
    return kit
}

export const jewelsFilterByType = (kit, type) => {  
    kit.filter = 'type'      
    // let jewels = []
    // kit.jewels.map(function(value){     
    //     if(value.tipo == type)   
    //         jewels.push(value)
    // })      
    // kit.jewels = jewels
    return kit
}