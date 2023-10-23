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
                withDotNet(sdk: '7.0') {
                    sh 'dotnet tool install --global Nuke.GlobalTool'
                }
            }
        }
        stage('Pack') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh 'nuke pack'
                }
            }
        }
        stage('Push') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh 'nuke push'
                }
            }
        }
    }
}