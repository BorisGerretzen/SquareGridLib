pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scmGit(
                    branches: [[name: '*/master']], 
                    extensions: [], 
                    userRemoteConfigs: [
                        [credentialsId: 'Github-SquareGridLib', url: 'git@github.com:BorisGerretzen/SquareGridLib.git']
                    ]
                )
            }
        }
        stage('Install nuke') {
            steps {
                sh 'dotnet tool install --global Nuke.GlobalTool'
            }
        }
        stage('Pack') {
            steps {
                sh 'nuke pack'
            }
        }
        stage('Push') {
            steps {
                sh 'nuke push'
            }
        }
    }
}
