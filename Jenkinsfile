pipeline {
    agent any

    environment {
        // Định nghĩa biến môi trường
        REMOTE_HOST = '192.168.64.100'  // Địa chỉ IP của máy chủ
        REMOTE_USER = 'linhpv'           // Tên người dùng để SSH
        REMOTE_PATH = '/var/www/myapp'   // Đường dẫn trên máy chủ nơi bạn muốn triển khai
        SSH_PASS = '4321'                // Mật khẩu SSH
    }

    stages {
        stage('Checkout') {
            steps {
                // Lấy mã nguồn từ Git
                checkout scm
            }
        }
        stage('Build') {
            steps {
                // Khôi phục và xây dựng ứng dụng
                sh 'dotnet restore'
                sh 'dotnet publish --no-restore --configuration Release --output ./publish'
            }
        }
        stage('Deploy') {
            steps {
                script {
                    // Kiểm tra và tạo thư mục nếu chưa tồn tại
                    sh """
                        sshpass -p '${SSH_PASS}' ssh -o StrictHostKeyChecking=no ${REMOTE_USER}@${REMOTE_HOST} 'mkdir -p ${REMOTE_PATH}'
                    """
        
                    // Sao chép các tệp đã xuất bản lên máy chủ
                    sh """
                        sshpass -p '${SSH_PASS}' scp -o StrictHostKeyChecking=no -r ./publish/* ${REMOTE_USER}@${REMOTE_HOST}:${REMOTE_PATH}
                    """
        
                    // Dừng ứng dụng nếu nó đang chạy
                    sh """
                        sshpass -p '${SSH_PASS}' ssh -o StrictHostKeyChecking=no ${REMOTE_USER}@${REMOTE_HOST} 'pkill -f QLCuaHangBanSach.dll || true'
                    """
        
                    // Khởi động lại ứng dụng trên máy chủ
                    sh """
                        sshpass -p '${SSH_PASS}' ssh -o StrictHostKeyChecking=no ${REMOTE_USER}@${REMOTE_HOST} 'cd ${REMOTE_PATH} && nohup dotnet QLCuaHangBanSach.dll > output.log 2>&1 &'
                    """
                }
            }
    }

        stage('Notify') {
            steps {
                // Thông báo về tình trạng triển khai
                echo 'Deployment completed!'
            }
        }
    }

    post {
        always {
            // Thông báo khi hoàn thành pipeline
            echo 'Pipeline finished.'
        }
        failure {
            // Thông báo khi có lỗi
            echo 'Build failed!'
        }
    }
}
