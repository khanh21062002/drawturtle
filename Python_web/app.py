import sys
from PyQt5 import QtCore, QtGui, QtWidgets, QtWebEngineWidgets

class WebBrowser(QtWidgets.QWidget):
    def __init__(self):
        super().__init__()

        # Create the QWebEngineView
        self.view = QtWebEngineWidgets.QWebEngineView(self)

        # Set the URL to a default value
        self.view.setUrl(QtCore.QUrl("https://www.google.com"))

        # Create the URL bar
        self.url_bar = QtWidgets.QLineEdit(self)
        self.url_bar.setPlaceholderText("Enter a URL")

        # Connect the URL bar to the QWebEngineView's load function
        self.url_bar.returnPressed.connect(self.load_url)

        # Create the back, forward, and refresh buttons
        self.back_button = QtWidgets.QPushButton("Back", self)
        self.forward_button = QtWidgets.QPushButton("Forward", self)
        self.refresh_button = QtWidgets.QPushButton("Refresh", self)

        # Connect the back and forward buttons to the QWebEngineView's back and forward functions
        self.back_button.clicked.connect(self.view.back)
        self.forward_button.clicked.connect(self.view.forward)

        # Connect the refresh button to the QWebEngineView's reload function
        self.refresh_button.clicked.connect(self.view.reload)

        # Create a horizontal layout for the URL bar and buttons
        controls_layout = QtWidgets.QHBoxLayout()
        controls_layout.addWidget(self.back_button)
        controls_layout.addWidget(self.forward_button)
        controls_layout.addWidget(self.refresh_button)
        controls_layout.addWidget(self.url_bar)

        # Create a vertical layout for the QWebEngineView and the controls
        layout = QtWidgets.QVBoxLayout(self)
        layout.addLayout(controls_layout)
        layout.addWidget(self.view)

        # Set the main layout of the widget
        self.setLayout(layout)

    def load_url(self):
        # Get the URL from the URL bar
        url = self.url_bar.text()

        # If the URL is not empty, set it as the QWebEngineView's URL
        if url:
            self.view.setUrl(QtCore.QUrl(url))

if __name__ == "__main__":
    app = QtWidgets.QApplication(sys.argv)
    browser = WebBrowser()
    browser.show()
    sys.exit(app.exec_())

