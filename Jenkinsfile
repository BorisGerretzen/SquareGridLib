pipeline {
    agent any
    stages {
        stage('Set path') {
            steps {
                sh "export PATH=${PATH}:${HOME}/.dotnet/tools"
            }
        }
        stage('Install nuke') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh 'dotnet tool install --global Nuke.GlobalTool | echo "already installed"'
                }
            }
        }
        stage('Pack') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh 'dotnet nuke pack'
                }
            }
        }
        stage('Push') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh 'dotnet nuke push'
                }
            }
        }
    }
}
