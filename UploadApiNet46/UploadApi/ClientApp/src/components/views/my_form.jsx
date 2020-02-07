import React from "react"
import axios from 'axios'

class MyForm extends React.Component {
    fileSelectedHandler = event => {
        this.setState({
            selectedFile: event.target.files[0]
        })
        console.log(event.target.files[0])
        console.log(`file size: ${event.target.files[0].size}`)
    }

    upLoadHandler = event => {
        let fd = new FormData()
        fd.append('image', this.state.selectedFile, this.state.selectedFile.name)
        console.log(fd)
        axios.post('http://localhost:50827/api/uploadfile', fd, {
            onUploadProgress: progressEvent => {
                console.log('Upload progress: ' + Math.round(progressEvent.loaded / progressEvent.total * 100) + '%')
            }
        })
        .then(res => {
            console.log(res)
        })
    }

    render() {
        return (
            <div>
                <h3>upload</h3>
                <p></p>
                <input type='file' onChange={this.fileSelectedHandler} />
                <button onClick={this.upLoadHandler}>Enviar</button>
            </div>

        )
    }
}

export default MyForm