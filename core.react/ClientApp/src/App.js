import { Route, Switch } from 'react-router-dom'
import Welcome from './components/welcome'
import Counter from './components/counter'

function App() {
  return (
    <>
      <h3>My App</h3>
      <h4>Welcome</h4>
      <Switch>  
        <Route exact path='/welcome' component={Welcome} />
        <Route exact path='/counter' component={Counter} />
      </Switch>
    </>
  );
}

export default App;
