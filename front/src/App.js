import React from 'react';
import logo from './logo.svg';
import './App.css';

class App extends React.Component{
  constructor(props){
    super(props);
    this.state={
      message: null,
      dataList: null,
      choosed: null,
    };
  }

  getData = ()=>{
    fetch('https://localhost:5001/api/Transfer',{
      method: 'GET'
    })
    .then(res =>res.json())
    .then(data=>{
      this.setState({dataList:data});
    });
  };

  // uploadFile = (e)=>{
  //   const file = e.target.files[0];
  //   const data = new FormData();
  //   data.append('file',file);

  //   fetch('https://localhost:5001/api/Transfer',{
  //     method: "POST",
  //     body: data,
  //     mode: "no-cors",
  //     headers: {
  //       "Content-Type": "multipart/form-data"
  //     }
  //   });
  //}

  searchDuePay=(e)=>{
    var number=e.target.id.replace("bank_","");
    console.log(number);
  };

  render(){
    var items=[];

    if(this.state.dataList!=null){
      this.state.dataList.forEach(element => {
        items.push(<tr>
                    <td>{element.duePay}</td>
                    <td>{element.bankTransfer}</td>
                    <td>
                      <button id={"bank_"+element.bankTransfer} onClick={this.searchDuePay}>Search</button>
                    </td>
                  </tr>)
      });
    }
    
    return (
      <div>
        <button onClick={this.getData}>Get Data</button>
        <table>
          <tbody>
              {items}
          </tbody>
        </table>
      </div>
    );
  }
}

export default App;
