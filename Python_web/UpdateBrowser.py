import sys
from qtpy import QtWidgets, QtWebEngineWidgets
from PyQt5.QtCore import QUrl

class MyWebBrowser(QtWidgets.QWidget):
    def __init__(self):
        super().__init__()

        # Create a QWebEngineView widget and set it as the central widget
        # of the main window
        self.view = QtWebEngineWidgets.QWebEngineView(self)
        self.view.setUrl(QUrl("http://www.google.com"))
        self.view.loadFinished.connect(self.adjustLocation)
        self.view.titleChanged.connect(self.adjustTitle)
        self.view.loadProgress.connect(self.setProgress)
        self.view.loadFinished.connect(self.finishLoading)

        # Add a QLineEdit widget for entering the URL
        self.locationEdit = QtWidgets.QLineEdit(self)
        self.locationEdit.setSizePolicy(
            QtWidgets.QSizePolicy.Expanding, self.locationEdit.sizePolicy().verticalPolicy()
        )
        self.locationEdit.returnPressed.connect(self.changeLocation)

        # Add a QToolBar widget for the back, forward, and refresh buttons
        self.toolbar = QtWidgets.QToolBar(self)
        self.addAction(self.view.pageAction(QtWebEngineWidgets.QWebEnginePage.Back))
        self.addAction(self.view.pageAction(QtWebEngineWidgets.QWebEnginePage.Forward))
        self.addAction(self.view.pageAction(QtWebEngineWidgets.QWebEnginePage.Reload))
        self.addAction(self.view.pageAction(QtWebEngineWidgets.QWebEnginePage.Stop))

        # Add a QTabWidget for opening multiple tabs
        self.tabs = QtWidgets.QTabWidget(self)
        self.tabs.setDocumentMode(True)
        self.tabs.setTabsClosable(True)
        self.tabs.tabCloseRequested.connect(self.closeTab)
        self.tabs.currentChanged.connect(self.currentTabChanged)

        # Add a button for creating new tabs
        self.newTabButton = QtWidgets.QPushButton("New Tab", self)
        self.newTabButton.clicked.connect(self.addTab)

        # Use a QVBoxLayout to arrange the widgets vertically
        layout = QtWidgets.QVBoxLayout(self)
        layout.addWidget(self.toolbar)
        layout.addWidget(self.locationEdit)
        layout.addWidget(self.view)
        layout.addWidget(self.tabs)
        layout.addWidget(self.newTabButton)
        
if __name__ == "__main__":

    app = QtWidgets.QApplication(sys.argv)
    browser = MyWebBrowser()
    browser.show()
    sys.exit(app.exec_())

