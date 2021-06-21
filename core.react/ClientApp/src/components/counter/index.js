import React, { useEffect, useState } from 'react'

const Counter = () => {

  const [forecasts, setForecasts] = useState();
  useEffect(async () => {
    const response = await fetch('api/sampledata/weatherforecasts', {
      method: 'GET',
      //headers: this.getHeaders(headers)
    });    
    setForecasts(await response.json())
  }, [])

  return (
    <>      
    {
      console.log('response', forecasts)
    }
      <h4>Counter page</h4>
      {forecasts.map(item => (
        <p>{item.summary}</p>
      ))}
    </>
  )
}

export default Counter