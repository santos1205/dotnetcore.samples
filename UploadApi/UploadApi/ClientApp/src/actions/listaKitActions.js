import axios from 'axios'
import {totalsProcess, updateJewel} from '../businessLayer/bl_listaKit'

const BASE_URL = 'https://jewels-api.herokuapp.com/api/jewels'

export const actjewelSell = (indexJewel, kit) => {  
  let jewelUpdate = kit.jewels[indexJewel].isSold == false ? true : false
  let kitUpd = updateJewel(kit, indexJewel)  
  // Load totals  
  kitUpd = totalsProcess(kitUpd)
  // Using Multi Middleware in order to update interface first then update base.
  return [    
    {type: "JEWEL_CHANGED", payload: kitUpd},
    actUpdateKitBase(kitUpd),    
  ]   
}

export const actFilterJewelsByType = (typeJewel) => {    
  return {    
      type: 'JEWELS_FILTERED', payload: typeJewel    
  }
}

export const actGetKits = () => {
  const request = axios.get(`${BASE_URL}`)  
  return {
    type: 'KITS_FECHED',
    payload: request
  }  
}

export const actGetKitById = (id) => {
  const request = axios.get(`${BASE_URL}/${id}`)  
  return {
    type: 'KITBYID_LOADED',
    payload: request
  }  
}

export const actGetFirstKit = () => {  
  const request = axios.get(`${BASE_URL}/firstElem`)  
  return {
    type: 'FIRST_KIT_LOADED',
    payload: request
  }  
}

// Private Actions ###########################################################

const actUpdateKitBase = (kit) => {
  return dispatch => {
    axios.put(`${BASE_URL}/${kit._id}`, {...kit})
      .then(resp => dispatch(actGetFirstKit()))
  }  
}