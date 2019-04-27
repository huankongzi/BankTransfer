import React from 'react';
import './App.css';
import {Button,Row,Col,Modal,List} from 'antd';

class App extends React.Component{
  constructor(props){
    super(props);
    this.state={
      message: null,
      dataList: null,
      choosed: null,
      searchTime: null,
      duePayList: null,
      modalVisible: false,
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
    if(number==="null"){
      return;
    }
    fetch("https://localhost:5001/api/Transfer/Search/"+number,{
      method: 'GET'
    })
    .then(res=>res.json())
    .then(data=>{
      this.setState({searchTime:data.timeSpan});
      this.setState({duePayList:data.resultList});
      this.setState({modalVisible:true});
    })
  };

  handleOk=(e)=>{
    this.setState({modalVisible:false});
  }

  handleCancel=(e)=>{
    this.setState({modalVisible:false});
  }

  render(){
    var items=[];

    if(this.state.dataList!=null){
      this.state.dataList.forEach(element => {
        items.push(<Row>
                    <Col span={4}>{element.duePay}</Col>
                    <Col span={4}>{element.bankTransfer}</Col>
                    <Col span={4}>
                      <Button id={"bank_"+element.bankTransfer}
                              type="primary" shape="circle" icon="search"
                              onClick={this.searchDuePay}></Button>
                    </Col>
                  </Row>)
      });
    }
    
    return (
      <div className="App">
          <Row>
            <Col span={12}>
              <Button type="primary" onClick={this.getData}>Get Data</Button>
            </Col>
          </Row>
            <Row>
              <Col span={5}>Due Pay</Col>
              <Col span={5}>Bank Transfer</Col>
              <Col span={2}></Col>
            </Row>
            {items}
            <Modal title="Search Result"
                  visible={this.state.modalVisible}
                  onOk={this.handleOk}
                  onCancel={this.handleCancel}>
              <p>Get the result cost {this.state.searchTime} milliseconds.</p>
              <p>The Due Pay are:</p>
              <p>
                <List
                  size="small"
                  bordered
                  dataSource={this.state.duePayList}
                  renderItem={item => (<List.Item>{item}</List.Item>)}
                />
              </p>
            </Modal>
      </div>
    );
  }
}

export default App;
