pipeline {
    agent any
    stages {
        stage('Install nuke') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh 'ls /var/jenkins_home/.dotnet/tools'
                    sh 'dotnet tool install --global Nuke.GlobalTool | echo "already installed"'
                }
            }
        }
        stage('Pack') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh "export PATH=${PATH}:${HOME}/.dotnet/tools"
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
