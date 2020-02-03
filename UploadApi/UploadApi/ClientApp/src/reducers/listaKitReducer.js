const INITIAL_STATE = {
  kit: {
    _id: "",
    cod: "",
    filter: "",
    jewels: []      
  }    
};

export default (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case "JEWEL_CHANGED":     
    return {...state, kit: action.payload};    
    case "KITS_FECHED":      
      return {...state, kit: action.payload.data};
    case "KITBYID_LOADED":      
      return {...state, kit: action.payload.data};
    case "FIRST_KIT_LOADED":      
      return {...state, kit: action.payload.data.value};      
    case "JEWELS_FILTERED":            
    return {
      kit: {
        ...state.kit, filter: action.payload
      }
    };
    default:
      return state;
  }
};
